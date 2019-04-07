export function RegisterUnityListener(methodName, callback) {
  window[methodName] = parameter => {
    callback(parameter);
  };

  if (typeof window.UnityListener === "undefined") window.UnityListener = {};

  window.UnityListener[methodName] = parameter => {
    callback(parameter);
  };
}
