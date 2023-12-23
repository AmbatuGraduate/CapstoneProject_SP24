import './style.scss'

import { Filter } from "../../Components/Filter/Filter"
import Listing from '../../Components/BodySection/ListingSection/Listing'
import SearchBar from '../../Components/SearchBar/SearchBar';

export const ManageTree = () => {
    return (
        <div className='manageTree'>
            <div className='searchArea flex'>
                <SearchBar className='seachBar' />
                <Filter className='filter' />
            </div>
            <div>
                <Listing />
            </div>
        </div>
    )
}