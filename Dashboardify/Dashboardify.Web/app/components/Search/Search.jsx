import React, { PropTypes } from 'react'

import TextField from 'material-ui/TextField'

import { SearchTextActions } from 'actions'
import { connect } from 'react-redux'

class Search extends React.Component {
  render () {
    let { dispatch, searchText } = this.props

    const style = {
      margin: 'auto',
      width: '100%'
    }

    return (
      <TextField
        type='text'
        hintText='Search'
        style={style}
        value={searchText}
        onChange={(e) => {
          // let text = e.target.value.toLowerCase()
          let text = e.target.value
          dispatch(SearchTextActions.setSearchText(text))
        }}
      />
    )
  }
}

Search.propTypes = {
  dispatch: PropTypes.func,
  searchText: PropTypes.string
}

export default connect(
  (state) => {
    return {
      searchText: state.searchText
    }
  }
)(Search)
