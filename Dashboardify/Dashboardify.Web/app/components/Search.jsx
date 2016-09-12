import React from 'react'
import { SearchTextActions } from 'actions';
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
            dispatch(SearchTextActions.setSearchText(text))
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
