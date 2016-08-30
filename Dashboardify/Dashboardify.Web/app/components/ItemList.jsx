import React from 'react';
import moment from 'moment';
import { connect } from 'react-redux';

import ItemListItem from 'ItemListItem';
import DashboardifyAPI from 'DashboardifyAPI';

class ItemList extends React.Component {
    render() {
        let {items, searchText, dashboardId} = this.props;
        let renderItems = () => {
            return DashboardifyAPI.filterItems(items, dashboardId, searchText).map((item) => {
                return (
                    <ItemListItem key={item.id} {...item}/>

                );
            });
        }

        return (
            <div className="row">
                {renderItems()}
            </div>
        );
    }
}


export default connect(
    (state) => {
        return {
            items: state.items,
            searchText: state.searchText
        }
    }
)(ItemList);
