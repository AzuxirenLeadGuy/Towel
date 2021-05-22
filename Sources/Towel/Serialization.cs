﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using Towel.DataStructures;

namespace Towel
{
	/// <summary>Static class containing serialization code.</summary>
	public static partial class Serialization
	{
		#region Shared

		internal static readonly XmlWriterSettings DefaultXmlWriterSettings =
			new() { OmitXmlDeclaration = true, };

		#endregion

		#region System.Xml.Serialization.XmlSerializer .NET Wrapper (Default)

		#region To XML

		/// <summary>Wrapper for the default XML serialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <returns>The XML serialzation of the value.</returns>
		public static string DefaultToXml<T>(T value) =>
			DefaultToXml(value, DefaultXmlWriterSettings);

		/// <summary>Wrapper for the default XML serialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="xmlWriterSettings">The settings of the XML writer during serialization.</param>
		/// <returns>The XML serialzation of the value.</returns>
		public static string DefaultToXml<T>(T value, XmlWriterSettings xmlWriterSettings)
		{
			using StringWriter stringWriter = new();
			DefaultToXml(value, stringWriter, xmlWriterSettings);
			return stringWriter.ToString();
		}

		/// <summary>Wrapper for the default XML serialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="textWriter">The text writer to output the XML serialization to.</param>
		public static void DefaultToXml<T>(T value, TextWriter textWriter) =>
			DefaultToXml(value, textWriter, DefaultXmlWriterSettings);

		/// <summary>Wrapper for the default XML serialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to serialize.</typeparam>
		/// <param name="value">The value to serialize.</param>
		/// <param name="textWriter">The text writer to output the XML serialization to.</param>
		/// <param name="xmlWriterSettings">The settings of the XML writer during serialization.</param>
		public static void DefaultToXml<T>(T value, TextWriter textWriter, XmlWriterSettings xmlWriterSettings)
		{
			using XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings);
			XmlSerializer serializer = new(typeof(T));
			serializer.Serialize(xmlWriter, value);
		}

		#endregion

		#region From XML

		/// <summary>Wrapper for the default XML deserialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to deserialize.</typeparam>
		/// <param name="string">The string containing the XML content to deserialize.</param>
		/// <returns>The deserialized value.</returns>
		public static T? DefaultFromXml<T>(string @string)
		{
			using StringReader stringReader = new(@string);
			return DefaultFromXml<T>(stringReader);
		}

		/// <summary>Wrapper for the default XML deserialization in .NET using XmlSerializer.</summary>
		/// <typeparam name="T">The type of object to deserialize.</typeparam>
		/// <param name="textReader">The text reader providing the XML to deserialize.</param>
		/// <returns>The deserialized value.</returns>
		public static T? DefaultFromXml<T>(TextReader textReader)
		{
			XmlSerializer serializer = new(typeof(T));
			return (T?)serializer.Deserialize(textReader);
		}

		#endregion

		#endregion

		#region Static Delegates

		#region XML

		internal static partial class StaticDelegateConstants
		{
			internal const string Name = "Delegate";
			internal const string DeclaringType = "Method.DeclaringType.AssemblyQualifiedName";
			internal const string MethodName = "Method.Name";
			internal const string Parameters = "Method.GetParameters";
			internal const string ParameterType = "ParameterType.AssemblyQualifiedName";
			internal const string ReturnType = "Method.ReturnType";
		}

		#region To XML

		/// <summary>Serializes a static delegate to XML.</summary>
		/// <typeparam name="T">The type of delegate to serialize.</typeparam>
		/// <param name="delegate">The delegate to serialize.</param>
		/// <returns>The XML serialization of the delegate.</returns>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static string StaticDelegateToXml<T>(T @delegate) where T : Delegate =>
			StaticDelegateToXml(@delegate, DefaultXmlWriterSettings);

		/// <summary>Serializes a static delegate to XML.</summary>
		/// <typeparam name="T">The type of delegate to serialize.</typeparam>
		/// <param name="delegate">The delegate to serialize.</param>
		/// <param name="xmlWriterSettings">The settings of the XML writer during serialization.</param>
		/// <returns>The XML serialization of the delegate.</returns>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static string StaticDelegateToXml<T>(T @delegate, XmlWriterSettings xmlWriterSettings) where T : Delegate
		{
			using StringWriter stringWriter = new();
			StaticDelegateToXml(@delegate, stringWriter, xmlWriterSettings);
			return stringWriter.ToString();
		}

		/// <summary>Serializes a static delegate to XML.</summary>
		/// <typeparam name="T">The type of delegate to serialize.</typeparam>
		/// <param name="delegate">The delegate to serialize.</param>
		/// <param name="textWriter">The text writer to output the XML serialization to.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static void StaticDelegateToXml<T>(T @delegate, TextWriter textWriter) where T : Delegate =>
			StaticDelegateToXml(@delegate, textWriter, DefaultXmlWriterSettings);

		/// <summary>Serializes a static delegate to XML.</summary>
		/// <typeparam name="T">The type of delegate to serialize.</typeparam>
		/// <param name="delegate">The delegate to serialize.</param>
		/// <param name="textWriter">The text writer to output the XML serialization to.</param>
		/// <param name="xmlWriterSettings">The settings of the XML writer during serialization.</param>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		public static void StaticDelegateToXml<T>(T @delegate, TextWriter textWriter, XmlWriterSettings xmlWriterSettings) where T : Delegate
		{
			using XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings);
			MethodInfo methodInfo = @delegate.Method;
			if (methodInfo.IsLocalFunction())
			{
				throw new NotSupportedException("delegates assigned to local functions are not supported");
			}
			if (!methodInfo.IsStatic)
			{
				throw new NotSupportedException("delegates assigned to non-static methods are not supported");
			}
			if (methodInfo.DeclaringType is null)
			{
				throw new ArgumentException($"{nameof(@delegate)}.{nameof(Delegate.Method)}.{nameof(MethodInfo.DeclaringType)} is null", nameof(@delegate));
			}
			ParameterInfo[] parameterInfos = methodInfo.GetParameters();
			xmlWriter.WriteStartElement(StaticDelegateConstants.Name);
			{
				xmlWriter.WriteStartElement(StaticDelegateConstants.DeclaringType);
				xmlWriter.WriteString(methodInfo.DeclaringType.AssemblyQualifiedName);
				xmlWriter.WriteEndElement();

				xmlWriter.WriteStartElement(StaticDelegateConstants.MethodName);
				xmlWriter.WriteString(methodInfo.Name);
				xmlWriter.WriteEndElement();

				xmlWriter.WriteStartElement(StaticDelegateConstants.Parameters);
				for (int i = 0; i < parameterInfos.Length; i++)
				{
					xmlWriter.WriteStartElement(StaticDelegateConstants.ParameterType);
					xmlWriter.WriteString(parameterInfos[i].ParameterType.AssemblyQualifiedName);
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteEndElement();

				xmlWriter.WriteStartElement(StaticDelegateConstants.ReturnType);
				xmlWriter.WriteString(methodInfo.ReturnType.AssemblyQualifiedName);
				xmlWriter.WriteEndElement();
			}
			xmlWriter.WriteEndElement();
		}

		#endregion

		#region From XML

		/// <summary>Deserializes a static delegate from XML.</summary>
		/// <typeparam name="T">The type of the delegate to deserialize.</typeparam>
		/// <param name="string">The string of XML content to deserialize.</param>
		/// <returns>The deserialized delegate.</returns>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when deserialization fails due to a return type mis-match.
		/// </exception>
		/// <exception cref="Exception">
		/// Thrown when deserialization fails. See the inner exception for more information.
		/// </exception>
		public static T StaticDelegateFromXml<T>(string @string) where T : Delegate
		{
			using StringReader stringReader = new(@string);
			return StaticDelegateFromXml<T>(stringReader);
		}

		/// <summary>Deserializes a static delegate from XML.</summary>
		/// <typeparam name="Delegate">The type of the delegate to deserialize.</typeparam>
		/// <param name="textReader">The text reader providing the XML to deserialize.</param>
		/// <returns>The deserialized delegate.</returns>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a non-static method.
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// Thrown when the delegate is pointing to a local function.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// Thrown when deserialization fails due to a return type mis-match.
		/// </exception>
		/// <exception cref="Exception">
		/// Thrown when deserialization fails. See the inner exception for more information.
		/// </exception>
		public static Delegate StaticDelegateFromXml<Delegate>(TextReader textReader) where Delegate : System.Delegate
		{
			try
			{
				string? declaringTypeString = null;
				string? methodNameString = null;
				IList<string> parameterTypeStrings = new ListArray<string>();
				string? returnTypeString = null;
				using (XmlReader xmlReader = XmlReader.Create(textReader))
				{
					while (xmlReader.Read())
					{
						CONTINUE:
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							switch (xmlReader.Name)
							{
								case StaticDelegateConstants.DeclaringType:
									declaringTypeString = xmlReader.ReadInnerXml();
									goto CONTINUE;
								case StaticDelegateConstants.MethodName:
									methodNameString = xmlReader.ReadInnerXml();
									goto CONTINUE;
								case StaticDelegateConstants.ParameterType:
									parameterTypeStrings.Add(xmlReader.ReadInnerXml());
									goto CONTINUE;
								case StaticDelegateConstants.ReturnType:
									returnTypeString = xmlReader.ReadInnerXml();
									goto CONTINUE;
							}
						}
					}
				}
				if (declaringTypeString is null)
				{
					goto ThrowMissingType;
				}
				if (returnTypeString is null)
				{
					goto ThrowMissingReturnType;
				}
				Type? declaringType = Type.GetType(declaringTypeString);
				Type? returnType = Type.GetType(returnTypeString);
				MethodInfo? methodInfo = null;
				if (parameterTypeStrings.Count > 0)
				{
					Type[] parameterTypes = parameterTypeStrings.Select(x => Type.GetType(x)).ToArray();
					methodInfo = declaringType.GetMethod(methodNameString, parameterTypes);
				}
				else
				{
					methodInfo = declaringType.GetMethod(methodNameString);
				}
				if (methodInfo is null)
				{
					goto ThrowMethodNotFound;
				}
				if (methodInfo.IsLocalFunction())
				{
					goto ThrowLocalFunctionException;
				}
				if (!methodInfo.IsStatic)
				{
					goto ThrowNonStaticException;
				}
				if (methodInfo.ReturnType != returnType)
				{
					goto ThrowReturnTypeMisMatchException;
				}
				return methodInfo.CreateDelegate<Delegate>();
			}
			catch (Exception exception)
			{
				throw new Exception("Deserialization failed.", exception);
			}
			ThrowMethodNotFound:
			throw new Exception("The method of the deserialization was not found.");
			ThrowMissingReturnType:
			throw new ArgumentException("Deserialization failed due to missing return type.");
			ThrowMissingType:
			throw new ArgumentException("Deserialization failed due to missing type.");
			ThrowNonStaticException:
			throw new NotSupportedException("Delegates assigned to non-static methods are not supported.");
			ThrowLocalFunctionException:
			throw new NotSupportedException("Delegates assigned to local functions are not supported.");
			ThrowReturnTypeMisMatchException:
			throw new InvalidOperationException("Deserialization failed due to a return type mis-match.");
		}

		#endregion

		#endregion

		#region JSON

		/// <summary>An object for the purposes of serializing static delegates.</summary>
		public static class Json
		{
			/// <summary>An object for the purposes of serializing static delegates.</summary>
			public class Delegate
			{
				/// <summary>The assemlby qualified declaring type of the method.</summary>
				public string DeclaringType { get; set; }
				/// <summary>The name of the method.</summary>
				public string MethodName { get; set; }
				/// <summary>The assembly qualified parameter types of the method.</summary>
				public string[] ParameterTypes { get; set; }
				/// <summary>The assembly qualified return type of the method.</summary>
				public string ReturnType { get; set; }
			}
		}

		#region To JSON

		/// <summary>Serializes a static delegate to JSON.</summary>
		/// <typeparam name="T">The type of delegate to serialize.</typeparam>
		/// <param name="delegate">The delegate to serialize.</param>
		/// <returns>The JSON serialization of the delegate.</returns>
		public static string StaticDelegateToJson<T>(T @delegate) where T : Delegate
		{
			MethodInfo methodInfo = @delegate.Method;
			if (methodInfo.IsLocalFunction())
			{
				throw new NotSupportedException("delegates assigned to local functions are not supported");
			}
			if (!methodInfo.IsStatic)
			{
				throw new NotSupportedException("delegates assigned to non-static methods are not supported");
			}
			ParameterInfo[] parameterInfos = methodInfo.GetParameters();
			string[] parameterTypes = new string[parameterInfos.Length];
			for (int i = 0; i < parameterTypes.Length; i++)
			{
				parameterTypes[i] = parameterInfos[i].ParameterType.AssemblyQualifiedName;
			}

			Json.Delegate delegateObject = new()
			{
				DeclaringType = methodInfo.DeclaringType.AssemblyQualifiedName,
				MethodName = methodInfo.Name,
				ParameterTypes = parameterTypes,
				ReturnType = methodInfo.ReturnType.AssemblyQualifiedName,
			};

			return JsonSerializer.Serialize(delegateObject);
		}

		#endregion

		#region From JSON

		/// <summary>Deserializes a static delegate from JSON.</summary>
		/// <typeparam name="Delegate">The type of the delegate to deserialize.</typeparam>
		/// <param name="string">The string of JSON content to deserialize.</param>
		/// <returns>The deserialized delegate.</returns>
		public static Delegate StaticDelegateFromJson<Delegate>(string @string) where Delegate : System.Delegate
		{
			Json.Delegate delegateObject = JsonSerializer.Deserialize<Json.Delegate>(@string);
			Type declaringType = Type.GetType(delegateObject.DeclaringType);
			Type returnType = Type.GetType(delegateObject.ReturnType);
			MethodInfo methodInfo = null;
			if (delegateObject.ParameterTypes.Length > 0)
			{
				Type[] parameterTypes = delegateObject.ParameterTypes.Select(x => Type.GetType(x)).ToArray();
				methodInfo = declaringType.GetMethod(delegateObject.MethodName, parameterTypes);
			}
			else
			{
				methodInfo = declaringType.GetMethod(delegateObject.MethodName);
			}
			if (methodInfo.IsLocalFunction())
			{
				goto ThrowLocalFunctionException;
			}
			if (!methodInfo.IsStatic)
			{
				goto ThrowNonStaticException;
			}
			if (methodInfo.ReturnType != returnType)
			{
				goto ThrowReturnTypeMisMatchException;
			}
			try
			{
				return methodInfo.CreateDelegate<Delegate>();
			}
			catch (Exception exception)
			{
				throw new Exception("Deserialization failed.", exception);
			}
		ThrowNonStaticException:
			throw new NotSupportedException("Delegates assigned to non-static methods are not supported.");
		ThrowLocalFunctionException:
			throw new NotSupportedException("Delegates assigned to local functions are not supported.");
		ThrowReturnTypeMisMatchException:
			throw new InvalidOperationException("Deserialization failed due to a return type mis-match.");
		}

		#endregion

		#endregion

		#endregion
	}
}
