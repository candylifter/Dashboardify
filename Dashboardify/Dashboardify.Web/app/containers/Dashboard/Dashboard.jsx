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
		let { isFetching, error } = this.props;
		let { dashboardId } = this.props.routeParams;

		dashboardId = parseInt(dashboardId);

		let renderItemList = () => {
			if (isFetching) {
				return (
					<div>
						<p className="text-center">Loading...</p>
					</div>
				)
			} else if (error === undefined) {
				return (
					<ItemList dashboardId={dashboardId}/>
				)
			} else {
				return (
					<p className="text-center">{error}</p>
				)
			}
		};

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
						{renderItemList()}
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
			isFetching: state.items.isFetching,
			error: state.items.error,
		}
	}
)(Dashboard);
