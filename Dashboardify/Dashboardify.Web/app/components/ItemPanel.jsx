import React from 'react';

class ItemPanel extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            isActive: this.props.item.isActive
        }
    }

    render() {
        let {item} = this.props;

        return (
            <div className="panel panel-default">
                <div className="panel-body">
                    <div className="row">
                        <div className="col-sm-12">
                            <a href="#" className="thumbnail">
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
                        <div className="col-sm-6 text-right"><input type="checkbox" checked={this.state.isActive} /></div>
                    </div>
                    <div className="row">
                        <div className="col-sm-6">
                            <span>Check interval</span>
                        </div>
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
                        <div className="col-sm-6">
                            <span>Last Checked</span>
                        </div>
                        <div className="col-sm-6 text-right">
                            <span className="text-muted">15 minutes ago</span>
                        </div>
                    </div>
                    <div className="row">
                        <div className="col-sm-6">
                            <span>Last Modified</span>
                        </div>
                        <div className="col-sm-6 text-right">
                            <span className="text-muted">1 hour ago</span>
                        </div>
                    </div>
                    <hr/>
                    <div className="row"></div>
                </div>
            </div>
        )
    }
}

export default ItemPanel;
