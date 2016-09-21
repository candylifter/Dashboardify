import React, { PropTypes } from 'react'
import moment from 'moment'
import { connect } from 'react-redux'

import { Card, CardHeader } from 'material-ui/Card'

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
    let { img, name, url, isSelected, lastModified } = this.props

    const style = {
      card: {
        display: 'inline-block',
        width: '15em',
        height: '15em',
        margin: '1em',
        // padding: '1em',
        textAlign: 'left',
        overflow: 'hidden'
      },
      header: {
        display: 'block',
        borderBottom: '1px solid #ccc',
        text: {
          display: 'block',
          padding: 0
        }
      },
      body: {
        height: '8.5em',
        overflow: 'hidden',
        position: 'relative',
        img: {
          position: 'absolute',
          // minWidth: '100%',
          // width: '100%'
          maxWidth: '100%',
          maxHeight: '100%'
          // transform: 'translate(-50%, 0%)'
        }
      },
      footer: {
        fontSize: '14px',
        color: 'rgba(0, 0, 0, 0.541176)',
        padding: '.5em 1em',
        borderTop: '1px solid #ccc'
      },
      image: {
        maxWidth: '100%',
        maxHeight: '100%'
      }
    }

    let zDepth = isSelected ? 2 : 1

    return (
      <Card style={style.card} onClick={this.handleSelect} zDepth={zDepth}>
        <CardHeader title={name} subtitle={ItemsAPI.extractDomain(url)} style={style.header} textStyle={style.header.text} />
        <div style={style.body}>
          <img src={img} style={style.body.img} />
        </div>
        <div style={style.footer}>
          <span>Last modified: {moment(lastModified).fromNow()}</span>
        </div>
      </Card>
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

// <Paper style={style.card} zDepth={1} onClick={this.handleSelect}>
//   <div style={style.header}>
//     <div style={style.header.name}>
//       <span>{name}</span>
//     </div>
//     <div style={style.header.url}>
//       <span>{url}</span>
//     </div>
//   </div>
//   <div style={style.body}>
//     <img style={style.image} src={img} alt={`Image of ${name}`} />
//   </div>
//   <div style={style.footer}>
//     <span>{lastChecked}</span>
//   </div>
// </Paper>

// <div className='col-xs-6 col-md-4 col-lg-3' onClick={() => dispatch(ItemsActions.selectItem(id, dashboardId))}>
//   <div className={panelClass}>
//     <div className='panel-heading'>{name}</div>
//     <div className='panel-body'>
//       <img src={img} alt={'Screenshot of ' + name} className='img-responsive' />
//     </div>
//     <div className='panel-footer'>Last modified: {moment(lastModified).fromNow()}</div>
//   </div>
// </div>
