import React from 'react';

import ItemList from 'ItemList';

class ItemDashboard extends React.Component {
    render() {

        let items = [
            {
                id: 1,
                img: 'https://www.placecage.com/gif/200/200'
            },
            {
                id: 2,
                img: 'https://www.placecage.com/gif/300/300'
            },
            {
                id: 3,
                img: 'https://www.placecage.com/gif/400/400'
            },
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
                          <div className="panel panel-default">
                            <div className="panel-body">
                              <div className="row">
                                <div className="col-sm-12">
                                  <a href="#" className="thumbnail">
                                    <img src="https://www.placecage.com/gif/605/205"/>
                                  </a>
                                </div>
                              </div>
                              <div className="row">
                                <div className="col-sm-6"><span><strong>Bulka</strong></span></div>
                                <div className="col-sm-6 text-right"><a href="#">Visit Website</a></div>
                              </div>
                              <hr />
                              <div className="row">
                                <div className="col-sm-6"><span>Active</span></div>
                                <div className="col-sm-6 text-right"><input type="checkbox" checked/></div>
                              </div>
                              <div className="row">
                                <div className="col-sm-6"><span>Check interval</span></div>
                                <div className="col-sm-6">
                                  <select className="form-control">
                                    <option>5 min.</option>
                                    <option>10 min.</option>
                                    <option>15 min.</option>
                                    <option>30 min.</option>
                                    <option>1 hour</option>
                                  </select>
                                </div>
                              </div>
                              <div className="row">
                                <div className="col-sm-6"><span>Last Checked</span></div>
                                <div className="col-sm-6 text-right"><span className="text-muted">15 minutes ago</span></div>
                              </div>
                              <div className="row">
                                <div className="col-sm-6"><span>Last Modified</span></div>
                                <div className="col-sm-6 text-right"><span className="text-muted">1 hour ago</span></div>
                              </div>
                              <hr />
                              <div className="row">

                              </div>
                            </div>
                          </div>
                        </div>
                    </div>
                </div>

            );
        }
    }

    export default ItemDashboard;
