import React from 'react';
import moment from 'moment';

import ItemListItem from 'ItemListItem';

class ItemList extends React.Component {

    render() {

        let {items} = this.props;

        let renderItems = () => {
            return items.map((item) => {
                return (
                    <ItemListItem key={item.id} {...item} itemClick={this.props.itemClick}/>
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



export default ItemList;
