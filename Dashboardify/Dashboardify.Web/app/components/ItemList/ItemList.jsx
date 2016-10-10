import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import { ItemListItem } from 'components'
import { ItemsAPI } from 'api'

class ItemList extends React.Component {
  render () {
    let { items, searchText, dashboardId } = this.props

    let renderItems = () => {
      if (items.length === 0) {
        return (
          <div className='flex-container flex-container--toolbar'>
            <div className='error'>
              <i className='error__icon material-icons'>dashboard</i>
              <p className='error__text'>No items yet</p>
              <p className='error__subtext'>Select some items with Dashboardify Chrome plugin to watch items here</p>
            </div>
          </div>
        )
      } else {
        return ItemsAPI.filterItems(items, dashboardId, searchText).map((item) => {
          return (
            <ItemListItem key={item.id} {...item} />
          )
        })
      }
    }

    return (
      <div>
        {renderItems()}
      </div>
    )
  }
}

ItemList.propTypes = {
  items: PropTypes.array,
  searchText: PropTypes.string,
  dashboardId: PropTypes.number
}

export default connect(
    (state) => {
      return {
        items: state.items.data,
        searchText: state.searchText
      }
    }
)(ItemList)
