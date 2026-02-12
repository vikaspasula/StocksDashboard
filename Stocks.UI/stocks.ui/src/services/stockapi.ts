import { type Stock } from "../models/stock";

const apiUrl = import.meta.env.VITE_Stocks_API_BASE_URL as string;

export async function fetchStocks(retries = 3): Promise<Stock[]> {
    try {
        const response = await fetch(`${apiUrl}/stocks`);
        if (!response.ok) throw new Error("API failed");
        return await response.json() as Stock[];
    } catch (error) {
        if (retries > 0) {
            await new Promise(res => setTimeout(res, 1000));
            return fetchStocks(retries - 1);
        }
        throw new Error("Failed to fetch stocks. Please try again later.");
    }
}