import React from 'react';

import TextField from 'material-ui/TextField';

import { SearchTextActions } from 'actions';
import { connect } from 'react-redux';

class Search extends React.Component {
  render () {
    let { dispatch, searchText } = this.props;

    const style = {
      margin: 'auto',
    }

    return (
        <TextField
          type="text"
          hintText="Search"
          style={style}
          value={searchText}
          onChange={(e) => {
            let text = e.target.value.toLowerCase();
            dispatch(SearchTextActions.setSearchText(text))
          }}
        />
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
