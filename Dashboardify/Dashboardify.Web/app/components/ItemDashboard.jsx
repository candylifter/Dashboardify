import React from 'react';
import lodash from 'lodash';

import ItemList from 'ItemList';
import ItemPanel from 'ItemPanel';

class ItemDashboard extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            items: [
                {
                    id: 1,
                    name: 'Nikoliukas',
                    img: 'https://www.placecage.com/gif/200/200',
                    url: '//www.autogidas.lt',
                    isActive: true,
                    isSelected: false,
                    checkInterval: '5 min',
                    lastChecked: 999,
                    lastModified: 1472129223
                },
                {
                    id: 2,
                    name: 'Nikoliukas nomer du',
                    img: 'https://www.placecage.com/gif/201/201',
                    url: '//www.autoplius.lt',
                    isActive: false,
                    isSelected: false,
                    checkInterval: '10 min',
                    lastChecked: 10000,
                    lastModified: 1472119223
                },
                {
                    id: 3,
                    name: 'Nikoliukas nomer tri',
                    img: 'https://www.placecage.com/gif/203/203',
                    url: '//www.autoplius.lt',
                    isActive: true,
                    isSelected: false,
                    checkInterval: '1 hour',
                    lastChecked: 10000,
                    lastModified: 1472129023
                }
            ],

            selectedItemId: 1

        }
    }

    handleItemClick(id) {
        let updatedItems = this.state.items.map((item) => {
            if (item.id === id) {
                item.isSelected = true;
            } else {
                item.isSelected = false;
            }

            return item;
        });

        this.setState({
            selectedItemId: id,
            items: updatedItems
        });
    }

    render() {

        let {items} = this.state;

        let getSelectedItem = (id) => {
            let itemId  = items.findIndex((item) => {
                if (item.id === id) {
                    return item;
                }
            });

            return itemId;
        }


        //Needs refactoring
        return (
            <div className="container-fluid">
                <div className="row">
                    <div className="col-md-6 col-lg-8">
                        <div className="row">
                            <div className="col-md-6">
                                <ol className="breadcrumb">
                                    <li>
                                        <a href="#">Home</a>
                                    </li>
                                    <li className="active">
                                        <a href="#">Audi</a>
                                    </li>
                                </ol>
                            </div>
                            <div className="col-md-6">
                                <div className="input-group">
                                    <input type="text" className="form-control" placeholder="Search for..."/>
                                        <span className="input-group-btn">
                                            <button className="btn btn-default" type="button">Go!</button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <hr/>
                            <ItemList items={items} itemClick={this.handleItemClick.bind(this)}/>
                        </div>
                        <div className="col-md-6 col-lg-4">
                            <ItemPanel item={items[getSelectedItem(this.state.selectedItemId)]}/>
                        </div>
                    </div>
                </div>

            );
        }
    }

    export default ItemDashboard;
