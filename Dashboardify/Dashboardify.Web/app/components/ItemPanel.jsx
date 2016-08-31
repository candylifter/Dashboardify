import React from 'react';
import {connect} from 'react-redux';
import moment from 'moment';

const actions = require('actions');
import CheckIntervalList from 'CheckIntervalList';

class ItemPanel extends React.Component {
    constructor(props, context) {
        super(props, context);
    }

    render() {

        let {items, dashboardId, dispatch} = this.props;

        let item = items.find((item) => {
            return item.isSelected && (item.dashboardId == dashboardId)
        });

        let renderPanel = () => {
            if (typeof item != 'undefined') {

                return (
                    <div className="panel-body">

                        <div className="row">
                            <div className="col-sm-12">
                                <a href="#" target="_blank" className="thumbnail">
                                    <img src={item.img}/>
                                </a>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <span>
                                    <strong>{item.name}</strong>
                                </span>
                            </div>
                            <div className="col-sm-6 text-right">
                                <a href={item.url}>Visit Website</a>
                            </div>
                        </div>
                        <hr/>
                        <div className="row">
                            <div className="col-sm-6">
                                <span>Active</span>
                            </div>
                            <div className="col-sm-6 text-right">
                                <input
                                    type="checkbox"
                                    checked={item.isActive}
                                    onChange={() => dispatch(actions.toggleItem(item.id))}
                                    />
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <span>Check interval</span>
                            </div>
                            <div className="col-sm-6">
                                <CheckIntervalList itemId={item.id}/>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <span>Last Checked</span>
                            </div>
                            <div className="col-sm-6 text-right">
                                <span className="text-muted">{moment(item.lastChecked).fromNow()}</span>
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-6">
                                <span>Last Modified</span>
                            </div>
                            <div className="col-sm-6 text-right">
                                <span className="text-muted">{moment(item.lastModified).fromNow()}</span>
                            </div>
                        </div>
                        <hr/>
                        <div className="row"></div>
                    </div>
                )
            } else {
                return (
                    <div className="panel-body">Select item to view properties</div>
                )
            }
        }

        return (
            <div className="panel panel-default">
                {renderPanel()}
            </div>
        )
    }
}

export default connect((state) => {
    return {items: state.items}
})(ItemPanel);
