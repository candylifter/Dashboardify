import React from 'react'
import {Link} from 'react-router';

const Dashboards = ({id, name, img}) => {
  return (
    <div className="col-xs-6 col-md-4 col-lg-2">
      <Link to={"/dashboard/"+id}>
        <div className="panel panel-default">
          <div className="panel-heading">{name}</div>
          <div className="panel-body">
            <img src={img} alt={"Screenshot of " + name} className="img-responsive"></img>
          </div>
        </div>
      </Link>
    </div>
  )
}

export default Dashboards
