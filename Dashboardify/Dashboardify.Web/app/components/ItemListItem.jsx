import React from 'react'
import moment from 'moment';
import { connect } from 'react-redux';

const actions = require('actions');

class ItemListItem extends React.Component {
	render() {
		let {id, dashboardId, img, name, isSelected, lastModified, dispatch} = this.props

		let panelClass = isSelected
			? "panel panel-primary"
			: "panel panel-default";
		return (
			<div className="col-xs-6 col-md-4 col-lg-3" onClick={() => dispatch(actions.selectItem(id, dashboardId))}>
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

export default connect()(ItemListItem);
