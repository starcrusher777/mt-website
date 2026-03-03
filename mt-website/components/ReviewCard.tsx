import React from 'react';
import Link from 'next/link';

interface ReviewCardProps {
    id: number;
    reviewer: {
        id: number;
        username: string;
        avatar?: string;
    };
    rating: number;
    date: string;
    text: string;
    dealType?: 'seller' | 'buyer';
    productImage?: string;
    productName?: string;
}

const ReviewCard: React.FC<ReviewCardProps> = ({
    reviewer,
    rating,
    date,
    text,
    dealType,
    productImage,
    productName
}) => {
    const renderStars = (rating: number) => {
        return Array.from({ length: 5 }, (_, i) => (
            <span key={i} style={{ color: i < rating ? '#FFD700' : '#444' }}>
                ★
            </span>
        ));
    };

    return (
        <div className="review-card">
            <div className="review-header">
                <Link href={`/user/${reviewer.id}`} className="review-avatar-link">
                    <div className="review-avatar">
                        {reviewer.avatar ? (
                            <img src={reviewer.avatar} alt={reviewer.username} />
                        ) : (
                            <span>{reviewer.username.charAt(0).toUpperCase()}</span>
                        )}
                    </div>
                </Link>
                <div className="review-info">
                    <div className="review-name-row">
                        <Link href={`/user/${reviewer.id}`} className="review-name">
                            {reviewer.username}
                        </Link>
                        {dealType && (
                            <span className={`deal-type-badge ${dealType}`}>
                                {dealType === 'seller' ? 'Seller' : 'Buyer'}
                            </span>
                        )}
                    </div>
                    <div className="review-meta">
                        <div className="review-rating">{renderStars(rating)}</div>
                        <span className="review-date">{new Date(date).toLocaleDateString()}</span>
                    </div>
                </div>
            </div>
            <p className="review-text">{text}</p>
            {productImage && productName && (
                <div className="review-product">
                    <img src={productImage} alt={productName} className="review-product-image" />
                    <span className="review-product-name">{productName}</span>
                </div>
            )}
        </div>
    );
};

export default ReviewCard;

