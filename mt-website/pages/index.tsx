import { useEffect, useState } from 'react';
import ProductCard from '../components/ProductCard';

interface Order {
    id: number;
    orderId: number;
    orderName: string;
    item: {
        name: string;
        price: number;
        images: { imageUrl: string }[];
    };
}

export default function HomePage() {
    const apiBase = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';
    const [orders, setOrders] = useState<Order[]>([]);
    const [activeTab, setActiveTab] = useState('all');
    const [popularOrders, setPopularOrders] = useState<Order[]>([]);
    const [recommendedOrders, setRecommendedOrders] = useState<Order[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (typeof window === 'undefined') return;

        setLoading(true);
        setError(null);

        (async () => {
            try {
                const res = await fetch('/api/orders');
                if (!res.ok) {
                    const body = await res.json().catch(() => ({}));
                    throw new Error(body.error || `HTTP ${res.status}`);
                }
                const data = await res.json();
                const shuffled = data.sort(() => 0.5 - Math.random());
                setOrders(shuffled);
                setPopularOrders(shuffled.slice(0, 6));
                setRecommendedOrders(shuffled.slice(6, 14));
            } catch (err) {
                console.error('Failed to fetch orders:', err);
                setError(err instanceof Error ? err.message : 'Failed to load orders.');
            } finally {
                setLoading(false);
            }
        })().catch(() => {});
    }, []);

    const tabs = [
        { id: 'all', label: 'All' },
        { id: 'anime', label: 'Anime' },
        { id: 'games', label: 'Games' },
        { id: 'music', label: 'Music' },
        { id: 'kpop', label: 'K-pop' }
    ];

    const filteredOrders = activeTab === 'all' 
        ? orders 
        : orders.filter(() => true); // TODO: Add actual filtering by category

    return (
        <div className="homepage-main-container">
            {/* Navigation Tabs */}
            <div className="nav-tabs">
                {tabs.map(tab => (
                    <button
                        key={tab.id}
                        className={`tab ${activeTab === tab.id ? 'active' : ''}`}
                        onClick={() => setActiveTab(tab.id)}
                    >
                        {tab.label}
                    </button>
                ))}
            </div>

            <div className="homepage">
                {loading && (
                    <p className="section-header" style={{ textAlign: 'center', padding: '2rem' }}>Loading…</p>
                )}
                {error && !loading && (
                    <p className="section-header" style={{ color: 'var(--error, #c00)', textAlign: 'center', padding: '2rem' }}>
                        {error}
                    </p>
                )}
                {/* Popular Today Section */}
                {popularOrders.length > 0 && (
                    <section style={{ marginBottom: '3rem' }}>
                        <h2 className="section-header">Popular Today</h2>
                        <div className="horizontal-scroll">
                            {popularOrders.map(order => (
                                <ProductCard
                                    key={order.id}
                                    id={order.id}
                                    image={
                                        order.item.images.length > 0
                                            ? `${apiBase}${order.item.images[0].imageUrl}`
                                            : '/placeholder.jpg'
                                    }
                                    title={order.orderName}
                                    fandom={order.item.name}
                                    price={order.item.price}
                                    href={`/ads/${order.id}`}
                                />
                            ))}
                        </div>
                    </section>
                )}

                {/* Recommendations Section */}
                {recommendedOrders.length > 0 && (
                    <section>
                        <h2 className="section-header">Recommendations for You</h2>
                        <div className="order-grid">
                            {recommendedOrders.map(order => (
                                <ProductCard
                                    key={order.id}
                                    id={order.id}
                                    image={
                                        order.item.images.length > 0
                                            ? `${apiBase}${order.item.images[0].imageUrl}`
                                            : '/placeholder.jpg'
                                    }
                                    title={order.orderName}
                                    fandom={order.item.name}
                                    price={order.item.price}
                                    href={`/ads/${order.id}`}
                                />
                            ))}
                        </div>
                    </section>
                )}

                {/* All Items Grid */}
                {filteredOrders.length > 0 && (
                    <section style={{ marginTop: '3rem' }}>
                        <h2 className="section-header">All Items</h2>
                        <div className="order-grid">
                            {filteredOrders.map(order => (
                                <ProductCard
                                    key={order.id}
                                    id={order.id}
                                    image={
                                        order.item.images.length > 0
                                            ? `${apiBase}${order.item.images[0].imageUrl}`
                                            : '/placeholder.jpg'
                                    }
                                    title={order.orderName}
                                    fandom={order.item.name}
                                    price={order.item.price}
                                    href={`/ads/${order.id}`}
                                />
                            ))}
                        </div>
                    </section>
                )}
            </div>
        </div>
    );
}