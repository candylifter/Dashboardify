import React, { PropTypes } from 'react'
import { connect } from 'react-redux'

import { ItemListItem } from 'components'
import { ItemsAPI } from 'api'

class ItemList extends React.Component {
  render () {
    let { items, searchText, dashboardId } = this.props

    const style = {
      display: 'flex',
      justifyContent: 'space-between',
      flexWrap: 'wrap',
      margin: '0 -1em',
      error: {
        width: '100%',
        minHeight: 'calc(100vh - 64px)',
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        textAlign: 'center',
        color: '#9E9E9E',
        icon: {
          fontSize: '8em'
        },
        text: {
          fontSize: '2em'
        },
        subtext: {
          margin: 0,
          fontSize: '1.5em'
        }
      }
    }

    let renderItems = () => {
      if (items.length === 0) {
        return (
          <div style={style.error}>
            <i className='material-icons' style={style.error.icon}>dashboard</i>
            <p style={style.error.text}>No dashboards yet</p>
            <p style={style.error.subtext}>Click the button in the bottom right to add one</p>
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
      <div style={style}>
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
