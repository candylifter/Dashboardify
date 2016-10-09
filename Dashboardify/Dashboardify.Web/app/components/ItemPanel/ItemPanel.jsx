import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import moment from 'moment'

import Paper from 'material-ui/Paper'
import Drawer from 'material-ui/Drawer'
import AppBar from 'material-ui/AppBar'
import IconButton from 'material-ui/IconButton'
import NavigationClose from 'material-ui/svg-icons/navigation/close'
import Toggle from 'material-ui/Toggle'
import FlatButton from 'material-ui/FlatButton'

import { ItemsActions, ItemPanelActions } from 'actions'
import { ItemsAPI } from 'api'
import { CheckIntervalList, ScreenshotSlider } from 'components'

class ItemPanel extends React.Component {
  constructor (props, context) {
    super(props, context)

    this.state = {open: false}

    this.handleDelete = this.handleDelete.bind(this)
    this.handleClose = this.handleClose.bind(this)
    this.handleToggle = this.handleToggle.bind(this)
    this.handleIntervalChange = this.handleIntervalChange.bind(this)
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
    const style = {
      padding: '0em 1em 1em',
      image: {
        maxWidth: '100%',
        margin: '1em 0',
        padding: '1em',
        img: {
          width: '100%',
          maxHeight: '300px',
          objectFit: 'cover'
        }
      },
      title: {

      },
      url: {
        margin: '1em 0',
        display: 'block',
        button: {
          width: '100%'
        }
      }

    }
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
            <div style={style}>
              <Paper style={style.image}>
                <img src={item.img} alt={`Screenshot of ${item.name}`} style={style.image.img} />
              </Paper>
              <div style={style.url}>
                <FlatButton href={item.url} target='_blank' label='Visit website' style={style.url.button} />
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
              <FlatButton label='Delete item' secondary style={style.url.button} onClick={this.handleDelete} />
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

//  let renderPanel = () => {
  //   if (typeof item !== 'undefined') {
  //     return (
  //       <p>yo</p>
  //   )
  //   } else {
  //     return (
  //       <div className='panel-body'>Select item to view properties</div>
  //     )
  //   }
  // }
// {renderPanel()}
//

// <div className='panel-body'>
//   <div className='row'>
//     <div className='col-sm-12'>
//       <a href='#' target='_blank' className='thumbnail'>
//         <img src={item.img} />
//       </a>
//     </div>
//   </div>
//   <div className='row'>
//     <div className='col-sm-6'>
//       <span>
//         <strong>{item.name}</strong>
//       </span>
//     </div>
//     <div className='col-sm-6 text-right'>
//       <a href={item.url}>Visit Website</a>
//     </div>
//   </div>
//   <hr />
//   <div className='row'>
//     <div className='col-sm-6'>
//       <span>Active</span>
//     </div>
//     <div className='col-sm-6 text-right'>
//       <input
//         type='checkbox'
//         checked={item.isActive}
//         onChange={() => dispatch(ItemsActions.toggleItem(item.id))}
//           />
//     </div>
//   </div>
//   <div className='row'>
//     <div className='col-sm-6'>
//       <span>Check interval</span>
//     </div>
//     <div className='col-sm-6'>
//       <CheckIntervalList itemId={item.id} />
//     </div>
//   </div>
//   <div className='row'>
//     <div className='col-sm-6'>
//       <span>Last Checked</span>
//     </div>
//     <div className='col-sm-6 text-right'>
//       <span className='text-muted'>{moment(item.lastChecked).fromNow()}</span>
//     </div>
//   </div>
//   <div className='row'>
//     <div className='col-sm-6'>
//       <span>Last Modified</span>
//     </div>
//     <div className='col-sm-6 text-right'>
//       <span className='text-muted'>{moment(item.lastModified).fromNow()}</span>
//     </div>
//   </div>
//   <hr />
//   <div className='row' />
// </div>
