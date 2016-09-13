import React from 'react'
import { connect } from 'react-redux';

import { Breadcrumb, Search, ItemList, ItemPanel } from 'components';

import { ItemsActions } from 'actions';

class Dashboard extends React.Component {
	componentWillMount() {
		let { dashboardId } = this.props.routeParams;
		let { dispatch } = this.props;

		dispatch(ItemsActions.fetchItems(parseInt(dashboardId)));
	}

	render() {

		let { items } = this.props;
		let { dashboardId } = this.props.routeParams;

		dashboardId = parseInt(dashboardId);

		return (
			<div className="container-fluid">
				<div className="row">
					<div className="col-md-6 col-lg-8">
						<div className="row">
							<div className="col-md-6">
								<Breadcrumb dashboardId={dashboardId}/>
							</div>
							<div className="col-md-6">
								<Search/>
							</div>
						</div>
						<hr/>
						<ItemList dashboardId={dashboardId}/>
					</div>
					<div className="col-md-6 col-lg-4">
						<ItemPanel dashboardId={dashboardId}/>
					</div>
				</div>
			</div>

		);
	}
}

export default connect(
	(state) => {
		return {
			items: state.items.data
		}
	}
)(Dashboard);
