import type { NextApiRequest, NextApiResponse } from 'next';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';

export default async function handler(
    req: NextApiRequest,
    res: NextApiResponse
) {
    if (req.method !== 'GET') {
        res.setHeader('Allow', 'GET');
        return res.status(405).json({ error: 'Method not allowed' });
    }

    const { id } = req.query;
    if (!id || Array.isArray(id)) {
        return res.status(400).json({ error: 'User id required' });
    }

    try {
        const response = await fetch(
            `${API_URL}/api/Order/GetOrdersByUserId/${id}?userId=${id}`
        );
        if (!response.ok) {
            return res.status(response.status).json({ error: 'Failed to get orders' });
        }
        const data = await response.json();
        res.status(200).json(data);
    } catch (err) {
        console.error('Orders by user proxy error:', err);
        res.status(502).json({ error: 'Failed to reach API.' });
    }
}
