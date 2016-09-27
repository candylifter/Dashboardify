import React from 'react'
import { Link } from 'react-router'

import Paper from 'material-ui/Paper'

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

// eslint-disable-next-line react/prop-types
const Dashboards = ({id, name, img}) => {
  return (
    <Link to={'/dashboard/' + id} style={style.link}>
      <Paper style={style.paper} zDepth={1}>
        <p>{name}</p>
      </Paper>
    </Link>
  )
}

export default Dashboards
