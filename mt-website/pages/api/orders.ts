import type { NextApiRequest, NextApiResponse } from 'next';

const API_URL = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';
const API_V1 = `${API_URL}/api/v1`;

export default async function handler(
    req: NextApiRequest,
    res: NextApiResponse
) {
    if (req.method !== 'GET') {
        res.setHeader('Allow', 'GET');
        return res.status(405).json({ error: 'Method not allowed' });
    }

    const page = req.query.page ?? '1';
    const pageSize = req.query.pageSize ?? '20';
    try {
        const response = await fetch(`${API_V1}/Order/GetOrders?page=${page}&pageSize=${pageSize}`);
        if (!response.ok) {
            const body = await response.json().catch(() => ({}));
            return res.status(response.status).json({
                error: body.errors?.[0]?.message ?? `API returned ${response.status}`,
            });
        }
        const body = await response.json();
        const data = body.data?.items ?? body.items ?? body;
        res.status(200).json(Array.isArray(data) ? data : body);
    } catch (err) {
        console.error('Orders proxy error:', err);
        res.status(502).json({
            error: 'Failed to reach API. Ensure the backend is running at ' + API_URL,
        });
    }
}
