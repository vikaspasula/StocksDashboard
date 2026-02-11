import React, { useEffect, useState } from "react";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

function Stocks() {

    const [stocks, setStocks] = useState([]);

    const fetchStocksWithRetry = async (
        retries = 3,
        delay = 1000) => {

        try {

            const response =
                await fetch("http://localhost:5025/api/stocks");

            if (!response.ok)
                throw new Error("API failed");

            return await response.json();

        }
        catch (error) {

            if (retries === 0)
                throw error;

            console.log("Retrying API...");

            await new Promise(r =>
                setTimeout(r, delay));

            return fetchStocksWithRetry(
                retries - 1,
                delay);
        }
    };

    useEffect(() => {

        async function loadStocks() {

            try {

                const data =
                    await fetchStocksWithRetry();

                setStocks(data);

            }
            catch (error) {

                console.error(error);

            }
        }

        loadStocks();

        // SignalR connection
        const connection = new HubConnectionBuilder()
            .withUrl("http://localhost:5025/stockhub")
            .withAutomaticReconnect()
            .build();

        connection.start();

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

    }, []);

    return (
        <table border="1">
            <thead>
                <tr>
                    <th>Symbol</th>
                    <th>Price</th>
                    <th>Time</th>
                </tr>
            </thead>

            <tbody>
                {stocks.map(s => (
                    <tr key={s.symbol}>
                        <td>{s.symbol}</td>
                        <td>{s.price}</td>
                        <td>{s.lastUpdated}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}

export default Stocks;
