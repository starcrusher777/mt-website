interface AdCardProps {
    title: string;
    description?: string;
    price: number;
    imageUrl?: string;
}

export default function AdCard({ title, description, price, imageUrl }: AdCardProps) {
    console.log("Image URL:", imageUrl);
    
    return (
        <div className="border rounded shadow p-4 flex flex-col">
            <img
                src={imageUrl || '/placeholder.jpg'}
                alt={title}
                className="h-48 w-full object-cover mb-4 rounded"
            />
            <h2 className="text-lg font-bold">{title}</h2>
            {description && <p className="text-sm text-gray-600 mb-2">{description}</p>}
            <p className="text-green-600 font-semibold">{price} ₽</p>
        </div>
    );
}