import { useRouter } from 'next/router';
import { useEffect, useState } from 'react';
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/navigation';
import { Navigation } from 'swiper/modules';
import Link from "next/link";

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
    user: {
        username: string;
    }
}

export default function AdDetailPage() {
    const router = useRouter();
    const { id } = router.query;
    const apiBase = process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5202';

    const [order, setOrder] = useState<Order | null>(null);
    const [currentUserId, setCurrentUserId] = useState<number | null>(null);
    const [selectedSize, setSelectedSize] = useState<string>('M');
    const [activeTab, setActiveTab] = useState<'description' | 'specs'>('description');

    useEffect(() => {
        const storedUserId = localStorage.getItem('userId');
        if (storedUserId) {
            setCurrentUserId(Number(storedUserId));
        }
    }, []);

    useEffect(() => {
        if (!id) return;
        fetch(`/api/order/${id}`)
            .then(res => res.json())
            .then(data => {
                setOrder(data);
            });
    }, [id]);
    
    if (!order) return <div className="ad-loading">Loading...</div>;

    const sizes = ['S', 'M', 'L', 'XL'];

    return (
        <div className="product-page-container">
            <div className="product-page-content">
                {/* Left: Image Gallery */}
                <div className="product-image-section">
                    <Swiper
                        modules={[Navigation]}
                        navigation
                        spaceBetween={10}
                        slidesPerView={1}
                        zoom={false}
                        className="product-swiper"
                    >
                        {order.item.images.map((img, index) => (
                            <SwiperSlide key={index}>
                                <img
                                    src={`${apiBase}${img.imageUrl}`}
                                    alt={`Image ${index + 1}`}
                                    className="product-main-image"
                                />
                            </SwiperSlide>
                        ))}
                    </Swiper>
                </div>

                {/* Right: Product Info */}
                <div className="product-info-section">
                    <h1 className="product-title">{order.orderName}</h1>
                    
                    {/* Rating (placeholder) */}
                    <div className="product-rating">
                        <span>★★★★★</span>
                        <span className="rating-text">(4.8)</span>
                    </div>

                    <div className="product-price">{order.item.price} ₽</div>

                    {/* Size Selection */}
                    <div className="size-selection">
                        <label className="size-label">Size:</label>
                        <div className="size-buttons">
                            {sizes.map(size => (
                                <button
                                    key={size}
                                    className={`size-button ${selectedSize === size ? 'active' : ''}`}
                                    onClick={() => setSelectedSize(size)}
                                >
                                    {size}
                                </button>
                            ))}
                        </div>
                    </div>

                    {/* Action Buttons */}
                    <div className="product-actions">
                        <button className="btn-primary" style={{ width: '100%' }}>
                            Add to Cart
                        </button>
                        <button className="btn-secondary" style={{ width: '100%' }}>
                            Buy Now
                        </button>
                    </div>

                    {/* User Info */}
                    <div className="product-user-info">
                        <span>Seller: </span>
                        <Link href={`/user/${order.userId}`} className="user-link">
                            {order.user.username}
                        </Link>
                    </div>

                    {currentUserId === order.userId && (
                        <Link href={`/order/edit?id=${id}`}>
                            <button className="btn-secondary" style={{ width: '100%', marginTop: '1rem' }}>
                                Edit Listing
                            </button>
                        </Link>
                    )}

                    {/* Tabs */}
                    <div className="product-tabs">
                        <button
                            className={`product-tab ${activeTab === 'description' ? 'active' : ''}`}
                            onClick={() => setActiveTab('description')}
                        >
                            Description
                        </button>
                        <button
                            className={`product-tab ${activeTab === 'specs' ? 'active' : ''}`}
                            onClick={() => setActiveTab('specs')}
                        >
                            Specifications
                        </button>
                    </div>

                    <div className="product-tab-content">
                        {activeTab === 'description' ? (
                            <p className="product-description">{order.item.description}</p>
                        ) : (
                            <div className="product-specs">
                                <div className="spec-item">
                                    <span className="spec-label">Item Name:</span>
                                    <span className="spec-value">{order.item.name}</span>
                                </div>
                                <div className="spec-item">
                                    <span className="spec-label">Condition:</span>
                                    <span className="spec-value">New</span>
                                </div>
                            </div>
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}