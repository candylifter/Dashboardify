import React from 'react';

import ItemList from 'ItemList';
import ItemPanel from 'ItemPanel';

class ItemDashboard extends React.Component {
    render() {

        let items = [
            {
                id: 1,
                name: 'Nikoliukas',
                img: 'https://www.placecage.com/gif/200/200',
                url: '//www.autogidas.lt',
                isActive: true,
                checkInterval: '5 min',
                lastChecked: 999,
                lastModified: 980
            },
            {
                id: 2,
                name: 'Nikoliukas nomer du',
                img: 'https://www.placecage.com/gif/201/201',
                url: '//www.autoplius.lt',
                isActive: false,
                checkInterval: '10 min',
                lastChecked: 10000,
                lastModified: 9000
            },
            {
                id: 3,
                name: 'Nikoliukas nomer tri',
                img: 'https://www.placecage.com/gif/203/203',
                url: '//www.autoplius.lt',
                isActive: true,
                checkInterval: '1 hour',
                lastChecked: 10000,
                lastModified: 9000
            }
        ]

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
                            <ItemList items={items}/>
                        </div>
                        <div className="col-md-6 col-lg-4">
                            <ItemPanel item={items[0]}/>
                        </div>
                    </div>
                </div>

            );
        }
    }

    export default ItemDashboard;
