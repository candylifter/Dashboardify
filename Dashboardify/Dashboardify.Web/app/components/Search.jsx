import React from 'react'
let actions = require('actions');
import { connect } from 'react-redux';

class Search extends React.Component {
  render () {
    let { dispatch, searchText } = this.props;
    return (
      <div className="form-group">
        <input
          ref="search"
          type="text"
          className="form-control"
          placeholder="Search"
          value={searchText}
          onChange={() => {
            let text = this.refs.search.value.toLowerCase();
            console.log(text);
            console.log(actions);
            dispatch(actions.setSearchText(text))
          }}
        />
      </div>
    )
  }
}

export default connect(
  (state) => {
    return {
      searchText: state.searchText
    }
  }
)(Search);


//() => this.props.onSearch(this.refs.search.value.toLowerCase())
