import React, { useState } from 'react';
import Link from 'next/link';
import { HeartIcon } from './Icons';

interface ProductCardProps {
  id: number;
  image: string;
  title: string;
  fandom?: string;
  price: number;
  isPreorder?: boolean;
  isOriginal?: boolean;
  href: string;
}

const ProductCard: React.FC<ProductCardProps> = ({
  id,
  image,
  title,
  fandom,
  price,
  isPreorder = false,
  isOriginal = false,
  href
}) => {
  const [isFavorite, setIsFavorite] = useState(false);

  const handleFavoriteClick = (e: React.MouseEvent) => {
    e.preventDefault();
    e.stopPropagation();
    setIsFavorite(!isFavorite);
  };

  return (
    <Link href={href} style={{ textDecoration: 'none' }}>
      <div className="order-card">
        <div className="order-image-container">
          <img
            src={image || '/placeholder.jpg'}
            alt={title}
            className="order-image"
          />
          {(isPreorder || isOriginal) && (
            <span className="product-tag">
              {isPreorder ? 'Pre-order' : 'Original'}
            </span>
          )}
          <button
            className={`favorite-button ${isFavorite ? 'active' : ''}`}
            onClick={handleFavoriteClick}
            aria-label="Add to favorites"
          >
            <HeartIcon filled={isFavorite} width={18} height={18} />
          </button>
        </div>
        <div className="order-card-content">
          <h2 className="order-title">{title}</h2>
          {fandom && <p className="order-item-name">{fandom}</p>}
          <p className="order-price">{price} ₽</p>
        </div>
      </div>
    </Link>
  );
};

export default ProductCard;

