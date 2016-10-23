import React, { PropTypes } from 'react'
import moment from 'moment'
import { connect } from 'react-redux'

import { CardHeader } from 'material-ui/Card'
import Paper from 'material-ui/Paper'

import { ItemsAPI } from 'api'
import { ItemsActions, ItemPanelActions } from 'actions'

class ItemListItem extends React.Component {
  constructor () {
    super()

    this.handleSelect = this.handleSelect.bind(this)
  }

  handleSelect () {
    const { id, dashboardId, img, name, url, isSelected, isActive, lastModified, dispatch } = this.props

    let item = {
      id, dashboardId, img, name, url, isSelected, lastModified, isActive
    }

    dispatch(ItemsActions.selectItem(id, dashboardId))
    dispatch(ItemPanelActions.set(item))
    dispatch(ItemPanelActions.open())
  }

  render () {
    let { img, name, url, isSelected, lastModified, lastChecked, isActive } = this.props

    let itemClassName = isSelected ? 'item item--selected' : 'item'
    itemClassName = isActive ? itemClassName : itemClassName + ' item--inactive'

    return (
      <Paper className={itemClassName} onClick={this.handleSelect} zDepth={1}>
        <CardHeader className='item__header' title={name} subtitle={ItemsAPI.extractDomain(url)} />
        <div className='item__media'>
          <img src={img} />
        </div>
        <div className='item__footer'>
          <span>Last checked: {moment(lastChecked).fromNow()}</span><br />
          <span>Content changed: {moment(lastModified).fromNow()}</span>
        </div>
      </Paper>
    )
  }
}

ItemListItem.propTypes = {
  id: PropTypes.number,
  dashboardId: PropTypes.number,
  name: PropTypes.string,
  url: PropTypes.string,
  img: PropTypes.string,
  isActive: PropTypes.bool,
  isSelected: PropTypes.bool,
  lastModified: PropTypes.string,
  lastChecked: PropTypes.string,
  dispatch: PropTypes.func
}

export default connect()(ItemListItem)
