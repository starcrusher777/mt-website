import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import React from 'react';
import Link from 'next/link';
import ProductCard from '../../components/ProductCard';
import ReviewCard from '../../components/ReviewCard';
import CollectionGallery from '../../components/CollectionGallery';
import StatCard from '../../components/StatCard';


interface User {
    id: number;
    username: string;
    contacts: {
        telephone: string | null;
        email: string | null;
        address: string | null;
    };
    socials: {
        telegram: string | null;
        vkontakte: string | null;
        instagram: string | null;
        twitter: string | null;
    };
    personals: {
        firstName: string | null;
        lastName: string | null;
    };
}

interface Order {
    id: number;
    orderId: number;
    orderName: string;
    userId: number;
    item: {
        name: string;
        description: string;
        price: number;
        images: { imageUrl: string }[];
    };
}

type TabType = 'for-sale' | 'collection' | 'history' | 'reviews';

export default function UserProfilePage() {
    const router = useRouter();
    const { id } = router.query;

    const apiBase = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';

    const [user, setUser] = useState<User | null>(null);
    const [orders, setOrders] = useState<Order[]>([]);
    const [collection, setCollection] = useState<Order[]>([]);
    const [reviews, setReviews] = useState<any[]>([]);
    const [activeTab, setActiveTab] = useState<TabType>('for-sale');

    const [currentUserId, setCurrentUserId] = useState<number | null>(null);
    
    // Mock data for stats (should come from API)
    const stats = {
        itemsForSale: orders.length,
        sold: 12,
        inCollection: 156,
        followers: 89,
        positiveReviews: 98, // percentage
        completedDeals: 47
    };

    useEffect(() => {
        const storedUserId = localStorage.getItem('userId');
        if (storedUserId) {
            setCurrentUserId(Number(storedUserId));
        }
    }, []);

    useEffect(() => {
        if (!id) return;
        
        fetch(`/api/user/${id}?userId=${id}`)
            .then(res => {
                if (!res.ok) {
                    throw new Error(`HTTP error! status: ${res.status}`);
                }
                return res.json();
            })
            .then(data => {
                setUser(data);
                
                fetch(`/api/orders/by-user/${id}?userId=${id}`)
                    .then(res => {
                        if (!res.ok) {
                            throw new Error(`HTTP error! status: ${res.status}`);
                        }
                        return res.json();
                    })
                    .then(data => setOrders(data))
                    .catch(error => {
                        console.error('Failed to fetch orders:', error);
                    });
            })
            .catch(error => {
                console.error('Failed to fetch user:', error);
            });
    }, [id]);

    if (!user) return <div className="ad-loading">Loading profile...</div>;

    // Extract hashtags from bio or create from interests
    const hashtags = ['#anime', '#kpop', '#merch', '#collector'];

    return (
        <div className="profile-page-container">
            {/* Cover/Header Section */}
            <div className="profile-cover">
                <div className="profile-cover-gradient" />
                <div className="profile-cover-content">
                    <div className="profile-avatar-large">
                        {user.username.charAt(0).toUpperCase()}
                    </div>
                    <div className="profile-header-info">
                        <h1 className="profile-display-name">
                            {user.personals.firstName && user.personals.lastName
                                ? `${user.personals.firstName} ${user.personals.lastName}`
                                : user.username}
                        </h1>
                        <p className="profile-username">@{user.username}</p>
                        {user.contacts.address && (
                            <p className="profile-location">{user.contacts.address}</p>
                        )}
                    </div>
                    <div className="profile-actions">
                        {currentUserId === user.id ? (
                            <Link href={`/profile/edit?id=${user.id}`}>
                                <button className="btn-primary">Edit Profile</button>
                            </Link>
                        ) : (
                            <button className="btn-primary">Send Message</button>
                        )}
                    </div>
                </div>
            </div>

            {/* Stats Bar */}
            <div className="profile-stats-bar">
                <StatCard label="Items for Sale" value={stats.itemsForSale} />
                <StatCard label="Sold" value={stats.sold} />
                <StatCard label="In Collection" value={stats.inCollection} />
                <StatCard label="Followers" value={stats.followers} />
            </div>

            {/* Trust Stats */}
            <div className="profile-trust-stats">
                <div className="trust-stat-item">
                    <div className="trust-stat-label">Positive Reviews</div>
                    <div className="trust-stat-progress">
                        <div className="trust-stat-progress-bar" style={{ width: `${stats.positiveReviews}%` }} />
                        <span className="trust-stat-value">{stats.positiveReviews}%</span>
                    </div>
                </div>
                <div className="trust-stat-item">
                    <div className="trust-stat-label">Completed Deals</div>
                    <div className="trust-stat-value-large">{stats.completedDeals}</div>
                </div>
            </div>

            {/* Bio Section */}
            <div className="profile-bio">
                <p className="profile-bio-text">
                    Collector of anime merch and K-pop albums. Love sharing my collection with the community!
                </p>
                <div className="profile-hashtags">
                    {hashtags.map((tag, index) => (
                        <span key={index} className="hashtag-chip">{tag}</span>
                    ))}
                </div>
                {/* Social Links */}
                <div className="profile-social-links">
                    {user.socials.instagram && (
                        <a href={user.socials.instagram} target="_blank" rel="noopener noreferrer" className="social-link">
                            Instagram
                        </a>
                    )}
                    {user.socials.telegram && (
                        <a href={user.socials.telegram} target="_blank" rel="noopener noreferrer" className="social-link">
                            Telegram
                        </a>
                    )}
                    {user.socials.vkontakte && (
                        <a href={user.socials.vkontakte} target="_blank" rel="noopener noreferrer" className="social-link">
                            VK
                        </a>
                    )}
                </div>
            </div>

            {/* Navigation Tabs */}
            <div className="profile-tabs">
                <button
                    className={`profile-tab ${activeTab === 'for-sale' ? 'active' : ''}`}
                    onClick={() => setActiveTab('for-sale')}
                >
                    Items for Sale
                </button>
                <button
                    className={`profile-tab ${activeTab === 'collection' ? 'active' : ''}`}
                    onClick={() => setActiveTab('collection')}
                >
                    Collection
                </button>
                <button
                    className={`profile-tab ${activeTab === 'history' ? 'active' : ''}`}
                    onClick={() => setActiveTab('history')}
                >
                    Deal History
                </button>
                <button
                    className={`profile-tab ${activeTab === 'reviews' ? 'active' : ''}`}
                    onClick={() => setActiveTab('reviews')}
                >
                    Reviews
                </button>
            </div>

            {/* Tab Content */}
            <div className="profile-tab-content">
                {activeTab === 'for-sale' && (
                    <div>
                        {orders.length === 0 ? (
                            <div className="no-listings">
                                <p>This user hasn't added any items for sale yet.</p>
                            </div>
                        ) : (
                            <div className="order-grid">
                                {orders.map(order => (
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
                        )}
                    </div>
                )}

                {activeTab === 'collection' && (
                    <div>
                        {collection.length === 0 ? (
                            <div className="no-listings">
                                <p>This user's collection is empty.</p>
                            </div>
                        ) : (
                            <CollectionGallery
                                items={collection.map(item => ({
                                    id: item.id,
                                    image: item.item.images.length > 0
                                        ? `${apiBase}${item.item.images[0].imageUrl}`
                                        : '/placeholder.jpg',
                                    title: item.orderName,
                                    href: `/ads/${item.id}`
                                }))}
                            />
                        )}
                    </div>
                )}

                {activeTab === 'history' && (
                    <div className="no-listings">
                        <p>Deal history will be displayed here.</p>
                    </div>
                )}

                {activeTab === 'reviews' && (
                    <div className="reviews-section">
                        {reviews.length === 0 ? (
                            <div className="no-listings">
                                <p>No reviews yet.</p>
                            </div>
                        ) : (
                            <div className="reviews-list">
                                {reviews.map((review, index) => (
                                    <React.Fragment key={review.id || index}>
                                        <ReviewCard {...review} />
                                        {index < reviews.length - 1 && <div className="review-divider" />}
                                    </React.Fragment>
                                ))}
                            </div>
                        )}
                    </div>
                )}
            </div>
        </div>
    );
}