import './style.scss'

import { TreeFilter } from "../../Components/FilterComponents/TreeFilter"
import TreeRow from '../../Components/BodySection/ListingSection/TreeRow'
import SearchBar from '../../Components/SearchBar'


export const ManageTree = () => {
    return (
        <div className='manageTree'>
            <div className='searchArea flex'>
                <div className='seachBar'>
                    <SearchBar />
                </div>
                <div className='filter flex'>
                    <TreeFilter />
                </div>
            </div>
            <div className='listing'>
                <TreeRow />
            </div>
        </div>
    )
}