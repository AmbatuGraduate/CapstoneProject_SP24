import './style.scss'

import { RouteFilter } from "../../Components/FilterComponents/RouteFilter"
import RouteRow from '../../Components/BodySection/ListingSection/RouteRow'
import SearchBar from '../../Components/SearchBar'


export const ManageRoute = () => {
    return (
        <div className='manageRoute'>
            <div className='searchArea flex'>
                <div className='seachBar'>
                    <SearchBar />
                </div>
                <div className='filter flex'>
                    <RouteFilter />
                </div>
            </div>
            <div className='listing'>
                <RouteRow />
            </div>
        </div>
    )
}