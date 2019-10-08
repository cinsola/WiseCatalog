import React from 'react';
import LinearProgress from '@material-ui/core/LinearProgress';
export default class SpinnerOrNot extends React.Component {
    render() {
        if (this.props.isSpinning) {
            return <LinearProgress />;
        } else {
            return <React.Fragment>{this.props.children}</React.Fragment>;
        }
    }
}