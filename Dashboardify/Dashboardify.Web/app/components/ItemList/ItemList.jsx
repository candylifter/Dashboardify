import React from 'react';
import moment from 'moment';
import { connect } from 'react-redux';

import { ItemListItem } from 'components';
import { ItemsAPI } from 'api';

class ItemList extends React.Component {
    render() {
        let {items, searchText, dashboardId} = this.props;
        let renderItems = () => {

            if (items.length === 0) {
                return (
                    <p className="text-center">No items in this dashboard</p>
                )
            } else {
                return ItemsAPI.filterItems(items, dashboardId, searchText).map((item) => {
                    return (
                        <ItemListItem key={item.id} {...item}/>
                    );
                });
            }
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
            items: state.items.data,
            searchText: state.searchText
        }
    }
)(ItemList);
