import React from 'react';
import moment from 'moment';

class Item extends React.Component {

    render() {
        let {id, img, name, isSelected, lastModified} = this.props

        let panelClass = isSelected ? "panel panel-primary" : "panel panel-default";
        return (
            <div className="col-xs-6 col-md-4 col-lg-3" onClick={() => this.props.itemClick(id)}>
                <div className={panelClass}>
                    <div className="panel-heading">{name}</div>
                    <div className="panel-body">
                        <img src={img} alt={"Screenshot of " + name} className="img-responsive"></img>
                    </div>
                    <div className="panel-footer">Last modified: {moment.unix(lastModified).fromNow()}</div>
                </div>
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
                    <Item key={item.id} {...item} itemClick={this.props.itemClick}/>
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
