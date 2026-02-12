import React, { useState, useEffect } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { type Stock } from "../models/stock";
import { fetchStocks } from "../services/stockApi";


const StocksGrid: React.FC = () => {
    const [stocks, setStocks] = useState<Stock[]>([]);
    const [error, setError] = useState<string>("");

    useEffect(() => {
        // Load API stocks
        const loadData = async () => {
            try {
                const data = await fetchStocks();
                setStocks(data);
                setError("");
            } catch (err: any) {
                setError(err.message);
            }
        };

        loadData();

        // Start SignalR connection
        const signalrHubUrl = import.meta.env.VITE_Stocks_SIGNALR_URL;
        const connection = new HubConnectionBuilder()
            .withUrl(`${signalrHubUrl}`)
            .withAutomaticReconnect()
            .build();
        connection.start()
            .then(() => console.log("SignalR connected"))
            .catch(() => setError("Failed to connect to live updates"));

        connection.onclose(() => setError("Live updates disconnected. Trying to reconnect..."));
        connection.onreconnecting(() => setError("Reconnecting to live updates..."));
        connection.onreconnected(() => setError(""));

        // Update stocks live
        connection.on(
            "ReceiveStockUpdate",
            stock => {

                setStocks(prev => {

                    const copy = [...prev];

                    const index =
                        copy.findIndex(
                            x => x.symbol === stock.symbol);

                    if (index >= 0)
                        copy[index] = stock;
                    else
                        copy.push(stock);

                    return copy;
                });

            });

        // Cleanup on unmount
        return () => {
            connection.stop();
        };
        
    }, []);

    return (
        <div style={{ padding: "1rem" }}>
            {error && <div style={{ color: "red", marginBottom: "1rem" }}> {error}</div>}
            {stocks.map(s => (
                <div key={s.symbol}>
                    {s.symbol}: {s.price}
                </div>
            ))}
        </div>
    );
};

export default StocksGrid;
