import React from 'react';

class Item extends React.Component {
    render() {
        let {id, img} = this.props
        return (
            <div key={id} className="col-xs-6 col-md-4 col-lg-3">
                <a href="#" className="thumbnail">
                    <img src={img} alt="..."/>
                </a>
            </div>
        )
    }
}

class ItemList extends React.Component {
    render() {

        let {items} = this.props;

        let renderItems = () => {
            return items.map((item) => {
                return (
                    <Item {...item}/>
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
