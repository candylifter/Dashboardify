import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import moment from 'moment'
import ImageZoom from 'react-medium-image-zoom'

import Paper from 'material-ui/Paper'
import Drawer from 'material-ui/Drawer'
import AppBar from 'material-ui/AppBar'
import IconButton from 'material-ui/IconButton'
import NavigationClose from 'material-ui/svg-icons/navigation/close'
import Toggle from 'material-ui/Toggle'
import FlatButton from 'material-ui/FlatButton'

import { ItemsActions, ItemPanelActions } from 'actions'
import { ItemsAPI } from 'api'
import { CheckIntervalList, ScreenshotSlider, ConfirmModal } from 'components'

class ItemPanel extends React.Component {
  constructor (props, context) {
    super(props, context)

    this.state = {
      open: false,
      confirmOpen: false
    }

    this.handleOpenConfirm = this.handleOpenConfirm.bind(this)
    this.handleCloseConfirm = this.handleCloseConfirm.bind(this)
    this.handleDelete = this.handleDelete.bind(this)
    this.handleClose = this.handleClose.bind(this)
    this.handleToggle = this.handleToggle.bind(this)
    this.handleIntervalChange = this.handleIntervalChange.bind(this)
  }

  handleOpenConfirm () {
    this.setState({confirmOpen: true})
  }

  handleCloseConfirm () {
    this.setState({confirmOpen: false})
  }

  handleDelete () {
    const { dispatch, items, dashboardId } = this.props

    let item = ItemsAPI.getSelectedItemDashboardId(items, dashboardId)

    dispatch(ItemsActions.deleteItem(item.id))
  }

  handleClose () {
    const { dispatch } = this.props

    dispatch(ItemPanelActions.close())
  }

  handleToggle () {
    const { items, dashboardId, dispatch } = this.props

    let item = ItemsAPI.getSelectedItemDashboardId(items, dashboardId)
    item.isActive = !item.isActive

    dispatch(ItemsActions.updateItem(item))
  }

  handleIntervalChange (event, index, value) {
    const { items, dashboardId, dispatch } = this.props
    let item = ItemsAPI.getSelectedItemDashboardId(items, dashboardId)

    item.checkInterval = value
    dispatch(ItemsActions.updateItem(item))
  }

  render () {
    let { items, dashboardId, open } = this.props

    let item = ItemsAPI.getSelectedItemDashboardId(items, dashboardId)

    let renderPanel = () => {
      if (item !== undefined) {
        return (
          <div>
            <AppBar
              iconElementLeft={<IconButton onClick={this.handleClose}><NavigationClose /></IconButton>}
              title={item.name}
              />
            <div className='item-panel'>
              <Paper className='item-panel__image'>
                <ImageZoom
                  image={{
                    src: item.img,
                    alt: `Screenshot of ${item.name}`
                  }}
                  shouldReplaceImage={false}
                />
              </Paper>
              <div className='item-panel__url'>
                <FlatButton href={item.url} target='_blank' label='Visit website' className='item-panel__button' />
              </div>
              <Toggle
                label='Active'
                toggled={item.isActive}
                onToggle={this.handleToggle}
                />
              <CheckIntervalList itemId={item.id} onChange={this.handleIntervalChange} />
              <p>Last checked: {moment(item.lastChecked).fromNow()}</p>
              <p>Content changed: {moment(item.lastModified).fromNow()}</p>
              <br /><br />
              <p>Previous content:</p>
              <ScreenshotSlider screenshots={item.screenshots} />
              <br /><br />
              <FlatButton label='Delete item' secondary className='item-panel__button' onClick={this.handleOpenConfirm} />
              <ConfirmModal
                open={this.state.confirmOpen}
                onConfirm={() => {
                  this.handleCloseConfirm()
                  this.handleDelete()
                }}
                onCancel={this.handleCloseConfirm}
                title='Delete item'
                text='Are you sure you want to delete this item?'
                confirmLabel='Delete'
                cancelLabel='Cancel'
                />
            </div>
          </div>
        )
      }
    }

    return (
      <Drawer openSecondary width={400} open={open}>
        {renderPanel()}
      </Drawer>
    )
  }
}

ItemPanel.propTypes = {
  items: PropTypes.array,
  dashboardId: PropTypes.number,
  open: PropTypes.bool,
  dispatch: PropTypes.func
}

export default connect((state) => {
  return {
    items: state.items.data,
    ...state.itemPanel
  }
})(ItemPanel)
