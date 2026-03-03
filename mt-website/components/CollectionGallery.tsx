import React, { useState } from 'react';
import Link from 'next/link';
import { HeartIcon, SearchIcon } from './Icons';

interface CollectionItem {
    id: number;
    image: string;
    title?: string;
    href: string;
}

interface CollectionGalleryProps {
    items: CollectionItem[];
}

const CollectionGallery: React.FC<CollectionGalleryProps> = ({ items }) => {
    const [hoveredItem, setHoveredItem] = useState<number | null>(null);

    return (
        <div className="collection-gallery">
            {items.map((item) => (
                <Link
                    key={item.id}
                    href={item.href}
                    className="collection-item"
                    onMouseEnter={() => setHoveredItem(item.id)}
                    onMouseLeave={() => setHoveredItem(null)}
                >
                    <div className="collection-item-image-container">
                        <img
                            src={item.image || '/placeholder.jpg'}
                            alt={item.title || 'Collection item'}
                            className="collection-item-image"
                        />
                        {hoveredItem === item.id && (
                            <div className="collection-item-overlay">
                                <button className="collection-item-action" title="View">
                                    <SearchIcon width={20} height={20} />
                                </button>
                                <button className="collection-item-action" title="Like">
                                    <HeartIcon width={20} height={20} />
                                </button>
                            </div>
                        )}
                    </div>
                    {item.title && (
                        <p className="collection-item-title">{item.title}</p>
                    )}
                </Link>
            ))}
        </div>
    );
};

export default CollectionGallery;

