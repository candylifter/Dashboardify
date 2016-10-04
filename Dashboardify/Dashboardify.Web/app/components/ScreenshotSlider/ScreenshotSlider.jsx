import React, { PropTypes } from 'react'
import Paper from 'material-ui/Paper'
import moment from 'moment'

class ScreenshotSlider extends React.Component {
  render () {
    let { screenshots } = this.props

    let renderScreenshots = () => {
      if (screenshots.length > 1) {
        return screenshots.map((screenshot, index) => {
          if (index !== 0) {
            return (
              <Paper key={index} className='screenshot-slider__item'>
                <img src={screenshot.ScrnshtURL} alt='Older screenshot' className='screenshot-slider__item__image' />
                <div className='screenshot-slider__item__header'>
                  <span>Screenshot taken {moment(screenshot.DateTaken).fromNow()}</span>
                </div>
              </Paper>
            )
          }
        })
      } else {
        return (
          <div>
            <h4>There are no older screenshots</h4>
          </div>
        )
      }
    }

    return (
      <div className='screenshot-slider'>
        {renderScreenshots()}
      </div>
    )
  }
}

ScreenshotSlider.propTypes = {
  screenshots: PropTypes.array
}

export default ScreenshotSlider
