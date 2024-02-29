import './style.scss'

import { RouteFilter } from "../../Components/FilterComponents/RouteFilter"
import EmployeeRow from '../../Components/BodySection/ListingSection/EmployeeRow'
import SearchBar from '../../Components/SearchBar'


export const ManageEmployee = () => {
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
                <EmployeeRow />
            </div>
        </div>
    )
}