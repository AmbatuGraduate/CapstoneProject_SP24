import './style.scss'

import { RouteFilter } from "../../Components/FilterComponents/RouteFilter"
import GarbageTruckRow from '../../Components/BodySection/ListingSection/GarbageTruckRow'
import SearchBar from '../../Components/SearchBar'


export const ManageGarbageTruck = () => {
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
                <GarbageTruckRow />
            </div>
        </div>
    )
}