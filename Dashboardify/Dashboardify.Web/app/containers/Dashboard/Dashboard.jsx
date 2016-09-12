import React from 'react'
import { connect } from 'react-redux';

import { Breadcrumb, Search, ItemList, ItemPanel } from 'components';

class Dashboard extends React.Component {
	handleToggleItem(id) {
		let updatedItems = this.state.items.map((item) => {
			if (item.id === id) {
				item.isActive = !item.isActive;
			}

			return item;
		});

		this.setState({items: updatedItems})
	}

	render() {

		let {items} = this.props;
		// let getSelectedItem = (id) => {
		// 	let itemId = items.findIndex((item) => {
		// 		if (item.id === id) {
		// 			return item;
		// 		}
		// 	});
		//
		// 	return itemId;
		// }

		return (
			<div className="container-fluid">
				<div className="row">
					<div className="col-md-6 col-lg-8">
						<div className="row">
							<div className="col-md-6">
								<Breadcrumb dashboardId={this.props.params.id}/>
							</div>
							<div className="col-md-6">
								<Search/>
							</div>
						</div>
						<hr/>
						<ItemList dashboardId={this.props.params.id}/>
					</div>
					<div className="col-md-6 col-lg-4">
						<ItemPanel dashboardId={this.props.params.id}/>
					</div>
				</div>
			</div>

		);
	}
}

export default connect(
	(state) => {
		return {
			items: state.items
		}
	}
)(Dashboard);
