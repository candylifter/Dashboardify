import React, { PropTypes } from 'react'
import moment from 'moment'
import { connect } from 'react-redux'

import Paper from 'material-ui/Paper'
import {Card, CardActions, CardHeader, CardMedia, CardTitle, CardText} from 'material-ui/Card'
import FlatButton from 'material-ui/FlatButton'

import { ItemsAPI } from 'api'
import { ItemsActions } from 'actions'

class ItemListItem extends React.Component {
  constructor () {
    super()

    this.handleSelect = this.handleSelect.bind(this)
  }

  handleSelect () {
    const { id, dashboardId, dispatch } = this.props

    dispatch(ItemsActions.selectItem(id, dashboardId))
  }

  render () {
    let { img, name, url, isSelected, lastChecked, lastModified, dispatch } = this.props

    const style = {
      card: {
        display: 'inline-block',
        width: '15em',
        height: '15em',
        margin: '1em',
        // padding: '1em',
        textAlign: 'left',
        border: isSelected ? '.2em solid rgb(0, 188, 212)' : 'none',
        overflow: 'hidden'
      },
      header: {
        display: 'block',
        text: {
          display: 'block',
          padding: 0
        }
      },
      body: {},
      footer: {},
      image: {
        maxWidth: '100%',
        maxHeight: '100%'
      }
    }

    return (
      <Card style={style.card} onClick={this.handleSelect}>
        <CardHeader title={name} subtitle={ItemsAPI.extractDomain(url)} style={style.header} textStyle={style.header.text} />
        <CardMedia>
          <img src={img} />
        </CardMedia>
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
