import React from 'react';
export default function WithLoading(WrappedComponent, loadingDelegate) {
    class LoadingComponent extends React.Component {
        constructor(props) {
            super(props);
            this.loadingDelegate = loadingDelegate;
            this.state = {
                isReduxHistoryPushing: false
            }
        }

        componentWillMount() {
            if ('history' in this.props && this.props.history.action == "PUSH") {
                this.setState({ isReduxHistoryPushing: true }, () => {
                    loadingDelegate(this);
                    this.setState({ isReduxHistoryPushing: false });
                });
            } else {
                loadingDelegate(this);
            }
        }

        render() {
            if (this.props.isLoading === true || this.state.isReduxHistoryPushing == true) {
                return (
                    <div>
                        <div>Loading...</div>
                    </div>
                );
            } else {
                return <WrappedComponent {...this.props} />;
            }
        }
    }

    return LoadingComponent;
}