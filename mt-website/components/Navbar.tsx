import Link from 'next/link';
import { useAuth } from '../components/AuthContext';
import { UserIcon, CartIcon, SearchIcon } from './Icons';
import { useState } from 'react';

const Navbar = () => {
    const { username, setUsername } = useAuth();
    const { userId, setUserId } = useAuth();
    const [searchQuery, setSearchQuery] = useState('');
    const [isMobileSearchOpen, setIsMobileSearchOpen] = useState(false);

    const handleLogout = () => {
        localStorage.removeItem('username');
        localStorage.removeItem('userid');
        setUsername(null);
        setUserId(null);
    };
    
    return (
        <header className="header">
            <div className="header-content">
                <Link href="/" className="logo">
                    FandomSwap
                </Link>
                
                <div className="search-container">
                    <SearchIcon className="search-icon" width={20} height={20} />
                    <input
                        type="text"
                        placeholder="Search for merch..."
                        className="search-input"
                        value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                    />
                </div>
                
                <div className="header-actions">
                    {username ? (
                        <>
                            <Link href="/createNew" className="icon-button" title="Create ad" style={{ fontSize: '0.875rem', padding: '0.5rem 1rem', borderRadius: '8px' }}>
                                Post listing
                            </Link>
                            <Link href={`/user/${userId}?userId=${userId}`} className="icon-button" title="Profile">
                                <UserIcon width={22} height={22} />
                            </Link>
                            <button className="icon-button" title="Cart">
                                <CartIcon width={22} height={22} />
                            </button>
                            <button 
                                className="icon-button" 
                                onClick={handleLogout}
                                title="Logout"
                                style={{ fontSize: '0.875rem', padding: '0.5rem 1rem', borderRadius: '8px' }}
                            >
                                Logout
                            </button>
                        </>
                    ) : (
                        <Link href="/auth" className="icon-button" style={{ fontSize: '0.875rem', padding: '0.5rem 1rem', borderRadius: '8px' }}>
                            Sign In
                        </Link>
                    )}
                </div>
            </div>
        </header>
    );
};

export default Navbar;