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

// <div className="col-xs-6 col-md-4 col-lg-2">
//   <Link to={"/dashboard/"+id}>
//     <div className="panel panel-default">
//       <div className="panel-heading">{name}</div>
//       <div className="panel-body">
//         <img src={img} alt={"Screenshot of " + name} className="img-responsive"></img>
//       </div>
//     </div>
//   </Link>
// </div>
