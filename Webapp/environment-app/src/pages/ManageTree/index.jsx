import './style.scss'

import { Filter } from "../../Components/Filter"
import Listing from '../../Components/BodySection/ListingSection/Listing'



export const ManageTree = () => {
    return (
        <>
            <div className='tool'>
                <Filter />
            </div>
            <div>
                <Listing />
            </div>
        </>
    )
}