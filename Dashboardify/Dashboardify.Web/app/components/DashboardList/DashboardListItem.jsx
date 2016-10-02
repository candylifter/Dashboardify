import React, { PropTypes } from 'react'
import { connect } from 'react-redux'
import { Link } from 'react-router'

import Paper from 'material-ui/Paper'
import FlatButton from 'material-ui/FlatButton'

import { DashboardsActions } from 'actions'

class Dashboards extends React.Component {
  constructor () {
    super()

    this.handleDelete = this.handleDelete.bind(this)
  }

  handleDelete (e) {
    e.preventDefault()

    let { dispatch, id } = this.props

    dispatch(DashboardsActions.deleteDashboard(id))
  }

  render () {
    let { id, name, img } = this.props

    const style = {
      link: {
        margin: 20,
        width: 300,
        maxWidth: 'calc(100vw - 40px)'
      },
      paper: {
        width: '100%',
        height: 300,
        textAlign: 'center',
        display: 'inline-block'
      }
    }

    return (
      <Link to={'/dashboard/' + id} style={style.link}>
        <Paper style={style.paper} zDepth={1}>
          <p>{name}</p>
          <FlatButton onClick={this.handleDelete} label='Delete' />
        </Paper>
      </Link>
    )
  }
}

Dashboards.propTypes = {
  id: PropTypes.number,
  name: PropTypes.string,
  img: PropTypes.string,
  dispatch: PropTypes.func
}

export default connect()(Dashboards)
