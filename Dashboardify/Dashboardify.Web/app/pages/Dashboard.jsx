import React from 'react'

import ItemList from 'ItemList';
import ItemPanel from 'ItemPanel';
import Breadcrumb from 'Breadcrumb';
import Search from 'Search';
import DashboardifyAPI from 'DashboardifyAPI';

class Dashboard extends React.Component {
	constructor(props, context) {
		super(props, context);
		this.state = {
			items: DashboardifyAPI.getItems(this.props.params.id),

			selectedItemId: undefined

		}
	}

	handleToggleItem(id) {
		let updatedItems = this.state.items.map((item) => {
			if (item.id === id) {
				item.isActive = !item.isActive;
			}

			return item;
		});

		this.setState({items: updatedItems})
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

		this.setState({selectedItemId: id, items: updatedItems});
	}

	handleSearch(searchText) {
		let updatedItems = [];
		if (searchText.length !== 0) {
			updatedItems = DashboardifyAPI.getItems(this.props.params.id).filter((item) => {
				return item.name.toLowerCase().indexOf(searchText) !== -1;
			});
		} else {
			updatedItems = DashboardifyAPI.getItems(this.props.params.id);
		}

		this.setState({
			items: updatedItems
		})

	}

	render() {

		let {items} = this.state;
		let getSelectedItem = (id) => {
			let itemId = items.findIndex((item) => {
				if (item.id === id) {
					return item;
				}
			});

			return itemId;
		}

		let renderItemPanel = () => {
			if (typeof this.state.selectedItemId === 'number') {
				return (<ItemPanel item={items[getSelectedItem(this.state.selectedItemId)]} toggleItem={this.handleToggleItem.bind(this)}/>)
			}
		}

		//Needs refactoring
		return (
			<div className="container-fluid">
				<div className="row">
					<div className="col-md-6 col-lg-8">
						<div className="row">
							<div className="col-md-6">
								<Breadcrumb dashboardId={this.props.params.id}/>
							</div>
							<div className="col-md-6">
								<Search onSearch={this.handleSearch.bind(this)}/>
							</div>
						</div>
						<hr/>
						<ItemList items={items} itemClick={this.handleItemClick.bind(this)}/>
					</div>
					<div className="col-md-6 col-lg-4">
						{renderItemPanel()}
					</div>
				</div>
			</div>

		);
	}
}

export default Dashboard;
