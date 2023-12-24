import './manageTree.scss'

import { Filter } from "../../Components/Filter/Filter"
import Listing from '../../Components/BodySection/ListingSection/Listing'
import SearchBar from '../../Components/SearchBar/SearchBar';
import { SearchBox } from '../../Components/SearchBox';


export const ManageTree = () => {
    return (
        <div className='manageTree'>
            <div className='searchArea flex'>
                <div className='seachBar'>
                    <SearchBar />
                </div>
                <div className='filter flex'>
                    <Filter />
                </div>
            </div>
            <div className='listing'>
                <Listing />
            </div>
        </div>
    )
}