import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import moment from 'moment'

import Drawer from 'material-ui/Drawer'
import AppBar from 'material-ui/AppBar'
import IconButton from 'material-ui/IconButton'
import NavigationClose from 'material-ui/svg-icons/navigation/close'
import Toggle from 'material-ui/Toggle'
import FlatButton from 'material-ui/FlatButton';

import { ItemsActions, ItemPanelActions } from 'actions'
import { CheckIntervalList } from 'components'

class ItemPanel extends React.Component {
  constructor (props, context) {
    super(props, context)

    this.state = {open: false}

    this.handleClose = this.handleClose.bind(this)
  }

  handleClose () {
    const { dispatch } = this.props

    dispatch(ItemPanelActions.close())
  }

  render () {
    let { item, open } = this.props

    const style = {
      padding: '0em 1em',
      image: {
        maxWidth: '100%',
        margin: '1em 0',
        img: {
          width: '100%'
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

    return (
      <Drawer openSecondary width={300} open={open}>
        <AppBar
          iconElementLeft={<IconButton onClick={this.handleClose}><NavigationClose /></IconButton>}
          title={'item.name'}
        />
        <div style={style}>
          <div style={style.image}>
            <img src={item.img} alt={`Screenshot of ${item.name}`} style={style.image.img} />
          </div>
          <div style={style.url}>
            <FlatButton href={item.url} target='_blank' label="Visit website" style={style.url.button} />
          </div>
          <Toggle
            label='Active'
            toggled={item.isActive}
          />
        </div>

      </Drawer>
    )
  }
}

ItemPanel.propTypes = {
  item: PropTypes.object,
  dashboardId: PropTypes.number,
  open: PropTypes.bool,
  dispatch: PropTypes.func
}

export default connect((state) => {
  return {
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
