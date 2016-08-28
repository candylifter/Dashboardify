import React from 'react'

class Search extends React.Component {
  render () {
    return (
      <div className="form-group">
        <input
          ref="search"
          type="text"
          className="form-control"
          placeholder="Search"
          onChange={() => this.props.onSearch(this.refs.search.value.toLowerCase())}
        />
      </div>
    )
  }
}

export default Search;
