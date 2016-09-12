import React from 'react';
import { connect } from 'react-redux';
import moment from 'moment';

// const actions = require('actions');
import { ItemsActions } from 'actions';

const CheckIntervalList = ({checkIntervals, items, itemId, dispatch}) => {

    let item = items.find((item) => {
        return item.id == itemId;
    });
    const renderIntervals = () => {
        return checkIntervals.map((interval) => {
            return (
                <option key={interval.id} value={interval.checkInterval}>
                    {moment.duration(interval.checkInterval).humanize()}
                </option>
            )
        });
    };

    return (
        <select
            className="form-control"
            value={item.checkInterval}
            onChange={(e) => dispatch(ItemsActions.setItemCheckInterval(item.id, e.target.value))}
            >
            {renderIntervals()}
        </select>
    )
}

export default connect(
    (state) => {
        return {
            items: state.items,
            checkIntervals: state.checkIntervals
        }
    }
)(CheckIntervalList);
