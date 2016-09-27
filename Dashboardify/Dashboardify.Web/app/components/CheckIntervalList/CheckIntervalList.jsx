import React from 'react'
import { connect } from 'react-redux'
import moment from 'moment'

import SelectField from 'material-ui/SelectField'
import MenuItem from 'material-ui/MenuItem'

import { ItemsActions } from 'actions'
import { ItemsAPI } from 'api'

// eslint-disable-next-line react/prop-types
const CheckIntervalList = ({ checkIntervals, itemId, items, dispatch }) => {
  const renderIntervals = () => {
    return checkIntervals.map((interval) => {
      return (
        <MenuItem key={interval.id} value={interval.checkInterval} primaryText={moment.duration(interval.checkInterval).humanize()} />
      )
    })
  }

  const item = ItemsAPI.getItemById(items, itemId)

  return (
    <div>
      <SelectField
        value={item.checkInterval}
        onChange={(event, index, value) => dispatch(ItemsActions.setItemCheckInterval(item.id, value))}
        floatingLabelText='Check interval'
        fullWidth
      >
        {renderIntervals()}
      </SelectField>
    </div>
    )
}

export default connect(
    (state) => {
      return {
        items: state.items.data,
        checkIntervals: state.checkIntervals
      }
    }
)(CheckIntervalList)
