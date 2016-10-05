import React from 'react'
import { connect } from 'react-redux'
import moment from 'moment'

import SelectField from 'material-ui/SelectField'
import MenuItem from 'material-ui/MenuItem'

import { ItemsActions } from 'actions'
import { ItemsAPI } from 'api'

class CheckIntervalList extends React.Component {
  render () {
    let { checkIntervals, itemId, items, dispatch } = this.props

    const renderIntervals = () => {
      return checkIntervals.map((interval) => {
        return (
          <MenuItem key={interval.id} value={interval.checkInterval} primaryText={moment.duration(interval.checkInterval).humanize()} />
        )
      })
    }

    const item = ItemsAPI.getItemById(items, itemId)
    //  onChange={(event, index, value) => dispatch(ItemsActions.setItemCheckInterval(item.id, value))}
    return (
      <div>
        <SelectField
          value={item.checkInterval}
          onChange={this.props.onChange}
          floatingLabelText='Check interval'
          fullWidth
        >
          {renderIntervals()}
        </SelectField>
      </div>
      )
  }
}

export default connect(
    (state) => {
      return {
        items: state.items.data,
        checkIntervals: state.checkIntervals
      }
    }
)(CheckIntervalList)
